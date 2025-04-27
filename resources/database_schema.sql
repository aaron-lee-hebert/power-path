-- Enable UUID extension
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Users table
CREATE TABLE users (
    user_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Exercise types
CREATE TABLE exercise_types (
    exercise_type_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(50) NOT NULL UNIQUE,
    category VARCHAR(50) NOT NULL,
    description TEXT
);

-- Training cycles
CREATE TABLE training_cycles (
    cycle_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID NOT NULL REFERENCES users(user_id) ON DELETE CASCADE,
    cycle_number INTEGER NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE,
    rounding_factor DECIMAL(5, 2) NOT NULL DEFAULT 2.5,
    notes TEXT,
    UNIQUE(user_id, cycle_number)
);

-- Workout weeks
CREATE TABLE workout_weeks (
    week_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    cycle_id UUID NOT NULL REFERENCES training_cycles(cycle_id) ON DELETE CASCADE,
    week_number INTEGER NOT NULL CHECK (week_number BETWEEN 1 AND 4),
    week_type VARCHAR(20) NOT NULL CHECK (week_type IN ('Standard', 'Deload')),
    UNIQUE(cycle_id, week_number)
);

-- User maxes
CREATE TABLE user_maxes (
    user_max_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID NOT NULL REFERENCES users(user_id) ON DELETE CASCADE,
    exercise_type_id UUID NOT NULL REFERENCES exercise_types(exercise_type_id),
    cycle_id UUID NOT NULL REFERENCES training_cycles(cycle_id) ON DELETE CASCADE,
    actual_one_rep_max DECIMAL(7, 2) NOT NULL,
    estimated_one_rep_max DECIMAL(7, 2),
    rounding_factor DECIMAL(5, 2) NOT NULL DEFAULT 2.5,
    date_recorded TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(user_id, exercise_type_id, cycle_id)
);

-- Workout days
CREATE TABLE workout_days (
    workout_day_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID NOT NULL REFERENCES users(user_id) ON DELETE CASCADE,
    week_id UUID NOT NULL REFERENCES workout_weeks(week_id) ON DELETE CASCADE,
    workout_date DATE NOT NULL,
    status VARCHAR(20) NOT NULL CHECK (status IN ('Planned', 'Completed', 'Skipped')),
    notes TEXT,
    UNIQUE(user_id, week_id, workout_date)
);

-- Programmed sets
CREATE TABLE programmed_sets (
    set_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    workout_day_id UUID NOT NULL REFERENCES workout_days(workout_day_id) ON DELETE CASCADE,
    exercise_type_id UUID NOT NULL REFERENCES exercise_types(exercise_type_id),
    percentage_of_tm DECIMAL(5, 2) NOT NULL,
    target_reps INTEGER NOT NULL,
    set_order INTEGER NOT NULL,
    is_amrap BOOLEAN NOT NULL DEFAULT FALSE,
    UNIQUE(workout_day_id, exercise_type_id, set_order)
);

-- Completed sets
CREATE TABLE completed_sets (
    completed_set_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    programmed_set_id UUID NOT NULL REFERENCES programmed_sets(set_id) ON DELETE CASCADE,
    actual_weight DECIMAL(7, 2) NOT NULL,
    actual_reps INTEGER NOT NULL,
    rpe INTEGER CHECK (rpe IS NULL OR (rpe BETWEEN 1 AND 10)),
    notes TEXT,
    UNIQUE(programmed_set_id)
);

-- Progress records (PR sheet)
CREATE TABLE progress_records (
    record_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID NOT NULL REFERENCES users(user_id) ON DELETE CASCADE,
    exercise_type_id UUID NOT NULL REFERENCES exercise_types(exercise_type_id),
    record_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    weight DECIMAL(7, 2) NOT NULL,
    reps INTEGER NOT NULL,
    is_personal_record BOOLEAN NOT NULL DEFAULT FALSE,
    UNIQUE(user_id, exercise_type_id, weight, reps)
);

-- Create indexes for performance
CREATE INDEX idx_training_cycles_user ON training_cycles(user_id);
CREATE INDEX idx_workout_weeks_cycle ON workout_weeks(cycle_id);
CREATE INDEX idx_workout_days_week ON workout_days(week_id);
CREATE INDEX idx_programmed_sets_workout ON programmed_sets(workout_day_id);
CREATE INDEX idx_progress_records_user_exercise ON progress_records(user_id, exercise_type_id);
CREATE INDEX idx_user_maxes_user_exercise ON user_maxes(user_id, exercise_type_id);

-- Function to calculate training max from 1RM
CREATE OR REPLACE FUNCTION calculate_training_max(one_rep_max DECIMAL, rounding_factor DECIMAL DEFAULT 2.5)
RETURNS DECIMAL AS $$
BEGIN
  RETURN FLOOR((one_rep_max * 0.9) / rounding_factor) * rounding_factor;
END;
$$ LANGUAGE plpgsql;

-- Function to estimate 1RM from weight and reps (Epley formula)
CREATE OR REPLACE FUNCTION estimate_one_rep_max(weight DECIMAL, reps INTEGER)
RETURNS DECIMAL AS $$
BEGIN
  IF reps = 1 THEN
    RETURN weight;
  ELSE
    RETURN weight * (1 + reps::DECIMAL / 30);
  END IF;
END;
$$ LANGUAGE plpgsql;

-- View for training maxes
CREATE VIEW view_training_maxes AS
SELECT 
    um.user_id,
    um.cycle_id,
    et.name AS exercise_name,
    um.actual_one_rep_max,
    calculate_training_max(um.actual_one_rep_max, um.rounding_factor) AS training_max
FROM 
    user_maxes um
JOIN 
    exercise_types et ON um.exercise_type_id = et.exercise_type_id;

-- View for scheduled workouts with sets
CREATE VIEW view_workout_plan AS
SELECT 
    wd.workout_day_id,
    u.username,
    tc.cycle_number,
    ww.week_number,
    et.name AS exercise_name,
    wd.workout_date,
    ps.set_order,
    ps.percentage_of_tm,
    ps.target_reps,
    ps.is_amrap,
    calculate_training_max(um.actual_one_rep_max, um.rounding_factor) AS training_max,
    ROUND(calculate_training_max(um.actual_one_rep_max, um.rounding_factor) * ps.percentage_of_tm / 100, 2) AS weight_to_use,
    CASE 
        WHEN ps.is_amrap THEN ps.target_reps || '+' 
        ELSE ps.target_reps::TEXT 
    END AS reps_display,
    wd.status
FROM 
    workout_days wd
JOIN 
    users u ON wd.user_id = u.user_id
JOIN 
    workout_weeks ww ON wd.week_id = ww.week_id
JOIN 
    training_cycles tc ON ww.cycle_id = tc.cycle_id
JOIN 
    programmed_sets ps ON wd.workout_day_id = ps.workout_day_id
JOIN 
    exercise_types et ON ps.exercise_type_id = et.exercise_type_id
LEFT JOIN 
    user_maxes um ON wd.user_id = um.user_id 
                   AND ps.exercise_type_id = um.exercise_type_id 
                   AND tc.cycle_id = um.cycle_id
ORDER BY 
    wd.workout_date, et.name, ps.set_order;

-- Insert initial exercise types with predefined UUIDs
INSERT INTO exercise_types (exercise_type_id, name, category, description) VALUES
('621a26fc-5d8f-4661-a170-52c8eda8c71b', 'Squat', 'Main', 'Barbell back squat'),
('3f184a45-9f3a-4cea-a187-d26a24edd7f2', 'Bench', 'Main', 'Barbell bench press'),
('5f69c790-4dab-4c5c-a141-8a3f64c35f5a', 'Deadlift', 'Main', 'Barbell conventional deadlift'),
('c0c69fc2-9e5a-4982-8c64-9c1814f4fe12', 'Press', 'Main', 'Standing overhead press'),
('d7bd8222-0eda-44c1-b946-2734b96dc7a6', 'Dips', 'Assistance', 'Bodyweight or weighted dips'),
('3fb71c73-a676-4c3d-a4c8-5346350b4792', 'Pull-ups', 'Assistance', 'Bodyweight or weighted pull-ups');