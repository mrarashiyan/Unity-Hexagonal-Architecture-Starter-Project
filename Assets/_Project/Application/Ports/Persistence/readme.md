How does Persistence work?
- Define Data object in Core layer
- Use IStorageVariable to convert them to a Savable object
- Concert them in Infrastructure layer, to define the Saving and Loading rules
- Finally, Initialize them with the correct IStorage concrete in StorageInstaller.cs