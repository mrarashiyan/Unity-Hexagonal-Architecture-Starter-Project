Installer scripts for Services are like these:
- Installer collect the dependencies and waits for the external dependencies to be finished
- it will pass them to Service Installer and waits for finishing the initialization
- no logic or rule, but the dependency and config injection happens in installer