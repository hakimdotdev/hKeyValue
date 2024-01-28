# hKeyValue

hKeyValue is a simple API for managing key-value pairs using Redis.

## Redis Configuration
The application currently uses a Testcontainers Redis instance for development and testing purposes. However, it's soon supported to use your own custom Redis instance.

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/hakimdotdev/hKeyValue.git
   ```

2. Navigate to the project directory:

   ```bash
   cd hKeyValue
   ```

3. Build and run the project:

   ```bash
   dotnet build
   dotnet run
   ```

## Usage

### Endpoints

#### Put Value
- URL: `/keyvalue/{key}`
- Method: `PUT`
- Description: Stores a value for the specified key.
- Request Body: Value to be stored.
- Response: Status code 201 (Created) if successful, 409 (Conflict) if the key already exists.

Example:

```bash
curl -v -L -X PUT -d "value_to_store" localhost:3000/api/keyvalue/mykey
```

#### Get Value
- URL: `/keyvalue/{key}`
- Method: `GET`
- Description: Retrieves the value associated with the specified key.
- Response: Value associated with the key if found, 404 (Not Found) if the key does not exist.

Example:

```bash
curl -v -L localhost:3000/api/keyvalue/mykey
```

#### Delete Value
- URL: `/keyvalue/{key}`
- Method: `DELETE`
- Description: Deletes the value associated with the specified key.
- Response: Status code 204 (No Content) if successful, 404 (Not Found) if the key does not exist.

Example:

```bash
curl -v -L -X DELETE localhost:3000/api/keyvalue/mykey
```

## License

This project is licensed under the [MIT License](LICENSE).
