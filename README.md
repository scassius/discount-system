# Discount System
Discount System 

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/discount-system.git
cd discount-system
```

### 2. Setup and Run the Backend

1. **Navigate to Backend Project**

    ```bash
    cd DiscountSystem
    ```

2. **Restore Dependencies**

    ```bash
    dotnet restore
    ```

3. **Apply Migrations**

    ```bash
    dotnet ef database update
    ```

4. **Run the Backend Server**

    ```bash
    dotnet run
    ```

    The server will start on:
    - `https://localhost:5001`
    - `http://localhost:5000`

### 3. Setup and Run the Client

1. **Open a New Terminal**

2. **Navigate to Client Project**

    ```bash
    cd DiscountSystem.Client
    ```

3. **Restore Dependencies**

    ```bash
    dotnet restore
    ```

4. **Run the Client Application**

    ```bash
    dotnet run
    ```

    The client will:
    - Generate 100 discount codes of length 8.
    - Attempt to use the code `ZZSAXX2F` (change to one of the codes generated and stored in the sqlite database).

## Project Structure

- **DiscountSystem.API**: Backend gRPC server.
- **DiscountSystem.Client**: Client application.
