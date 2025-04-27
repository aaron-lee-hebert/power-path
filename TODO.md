# Development Roadmap

## Phase 1: Setup & Foundation (2-3 weeks)

1. **Project Setup**
    - [X] Create ASP.NET Core MVC project (04/25/25)
    - [X] Configure PostgreSQL connection (04/25/25)
    - [X] Set up Entity Framework Core with database-first approach (04/25/25)
    - [X] Install necessary NuGet packages (04/25/25)
    - [X] Create repository on GitHub with branching strategy (04/25/25)
2. **Initial Database Implementation**
    - [ ] Create PostgreSQL tables based on entities
      - [ ] exercise_types
      - [ ] training_cycles
      - [ ] workout_weeks
      - [ ] user_maxes
      - [ ] workout_days
      - [ ] programmed_sets
      - [ ] completed_sets
      - [ ] progress_records
    - [ ] Create PostgreSQL indexes and other entities
    - [ ] Generate/update EF Core models from database
    - [ ] Create database context class
    - [X] Set up migrations (04/25/25)
3. **Core Architecture**
    - [ ] Implement central package management in Visual Studio
    - [ ] Implement repository pattern for data access
    - [ ] Create service layer for business logic
    - [ ] Set up dependency injection
    - [X] Configure authentication & authorization (ASP.NET Identity) (04/25/25)
4. **Deployment**
    - [ ] Create "staging" environment for testing
    - [ ] Create "production" environment for full deployment
    - [ ] Setup Docker to handle containerization

## Phase 2: Feature Development (4-6 weeks)

1. **User Management**
    - [ ] Registration & login
    - [ ] Profile management
    - [ ] Password recovery
    - [ ] User roles (if needed)
2. **Training Program Setup**
    - [ ] Create/manage training cycles
    - [ ] One-rep max input/calculation
    - [ ] Training max calculations
    - [ ] Weights & percentages system
3. **Workout Generation**
    - [ ] Weekly workout templates
    - [ ] Auto-generate workouts based on user's training maxes
    - [ ] Implement proper rounding of weights
    - [ ] Support for different variants (5/3/1 BBB, FSL, etc.)
4. **Workout Logging**
    - [ ] Record completed sets/reps
    - [ ] Track AMRAP sets
    - [ ] Log assistance exercises
    - [ ] Notes functionality

## Phase 3: Advanced Features (2-4 weeks)

1. **Progress Tracking**
    - [ ] PR tracking system
    - [ ] E1RM calculations
    - [ ] Progress charts/visualizations
    - [ ] Analytics dashboard
2. **User Experience Enhancements**
    - [ ] Mobile-friendly UI
    - [ ] Plate calculator
    - [ ] Weight conversion (kg/lbs)
    - [ ] Rest timer
    - [ ] Workout history
3. **Social Features** (optional)
    - [ ] Follow other users
    - [ ] Share workouts/PRs
    - [ ] Comments/likes

## Phase 4: Testing & Refinement (2-3 weeks)

1. **Testing**
    - [ ] Unit tests for cd
    - [ ] Integration tests for database operations
    - [ ] UI/UX testing
    - [ ] Performance testing
2. **Optimization**
    - [ ] Database query optimization
    - [ ] Caching implementation
    - [ ] API performance improvements

## Phase 5: Deployment & Operations (1-2 weeks)

1. **Deployment Preparation**
    - [ ] Configure CI/CD pipeline
    - [ ] Create new VPS in Vultr or Digital Ocean (LEMP Stack)
    - [ ] Database migration strategy
    - [ ] Logging & monitoring
2. **Launch**
    - [ ] Deploy to production
    - [ ] Monitor for issues
    - [ ] Collect initial user feedback
3. **Post-Launch**
    - [ ] Bug fixes
    - [ ] Performance improvements
    - [ ] Feature enhancements based on feedback

## Technology Stack

- [X] **Backend**: ASP.NET Core 9 MVC
- [X] **Database**: PostgreSQL 17
- [X] **ORM**: Entity Framework Core 9
- [X] **Authentication**: ASP.NET Identity
- [ ] **Frontend**:
    - [ ] Bootstrap 5 or Tailwind CSS
    - [ ] JavaScript/TypeScript
    - [ ] Potentially Alpine.js or minimal React for interactive components
- [ ] **Charting**: Chart.js or D3.js for visualizations
- [ ] **Hosting**: Azure App Service or similar
- [ ] **DevOps**: GitHub Actions or Azure DevOps

## Development Practices

1. **Documentation**
    - Clear code comments
    - API documentation
    - Setup instructions
    - Help section for users
2. **Code Quality**
    - Consistent coding standards
    - Static code analysis tools
3. **Agile Approach**
    - Two-week sprints
    - Prioritize features by user impact
    - Regular backlog refinement

## Key Implementation Considerations

1. **Database Performance**
    - Leverage the PostgreSQL functions and views we created
    - Use EF Core efficiently to avoid N+1 query problems
    - Consider indexing heavily-queried columns
2. **Scale-Ready Architecture**
    - Use caching for workout templates and configurations
    - Design API endpoints with potential mobile app in mind
    - Separate business logic from presentation
3. **User Experience**
    - Focus on simplicity for workout logging (minimize clicks)
    - Fast loading times for workout screens
    - Offline capability for the gym (consider PWA features)
