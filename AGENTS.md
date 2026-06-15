# Project overview

SnowPro is a multi-service project. The repository includes:

- `AuthorizationService`: authentication, registration, JWT, users, roles, tokens, and audit data.
- `LessonService`: lesson management, students, instructors, lesson statuses, and lesson-related notifications.
- `ProfileService`: client and instructor profiles, achievements, training levels, positions, and sport equipment metadata.
- `NotificationBroker`: routes notification messages to concrete notification channels.
- `NotificationSender`: notification sender services, including `NotificationEmailSender`, `NotificationSmsSender`, and `NotificationTelegramSender`.
- `SnowPro.WebUI`: React frontend for authentication, profiles, lessons, and dashboard flows.

# Working rules

- Before non-trivial code changes, analyze the related files first and briefly explain the plan to the user. For small obvious fixes, a short note about what changed is enough.
- Do not make broad refactorings without separate permission.
- Do not rename public API, classes, methods, DTOs, or endpoints without an explicit request.
- Do not translate file names, class names, method names, endpoints, or config keys.
- Keep changes scoped to the requested behavior and the directly related files.
- After making changes, list all changed files in the response.
- User-facing responses must be in Russian.

# C# style

- Use modern, concise C# style.
- Prefer `var` for local variables when the type is obvious from the right-hand side.
- Keep existing public contracts stable unless the user explicitly asks to change them.
- Write C# code comments in English.
- Add comments only when they clarify non-obvious behavior.

# Frontend style

- Follow the existing React structure and naming.
- Keep frontend changes focused on the requested feature or fix.
- Do not rename exported constants, components, routes, or config keys without an explicit request.
- API URLs are configured in `SnowPro.WebUI/src/config.js`.
- Authenticated Profile/Lesson requests should prefer existing axios configuration and helpers when present.

# Configuration rules

- Do not change `docker-compose.yml` without separate permission, unless the user explicitly requests that configuration change.
- Do not change any `appsettings.json` file without separate permission, unless the user explicitly requests that configuration change.
- Do not change frontend config, including `SnowPro.WebUI/src/config.js`, without separate permission, unless the user explicitly requests that configuration change.
- When configuration is relevant, first identify whether the value comes from `appsettings.json`, environment variables, Docker Compose, or frontend config.
- Docker environment variables may override nested config values using the `Section__Key` pattern.

# Validation

- If tests or build can be run, run them and report the exact commands used.
- If validation cannot be run, explain why.
- Prefer targeted validation for small changes and broader builds/tests for shared behavior or cross-service changes.
- For local service changes, prefer build/test only for the affected service.
- For shared contracts or cross-service behavior, run broader validation when possible.
- Do not claim validation passed unless it was actually run.

# Response format

- Respond to the user in Russian.
- Include a concise summary of what changed.
- Include the list of changed files.
- Include validation results with exact commands when commands were run.
- If no validation was run, state that clearly and explain why.
