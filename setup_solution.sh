#!/bin/bash

# Exit immediately if a command exits with a non-zero status
set -e

# Define solution name
SOLUTION_NAME="GestionCursos"

# Create solution file if it doesn't exist
if [ ! -f "$SOLUTION_NAME.sln" ]; then
    dotnet new sln -n "$SOLUTION_NAME" --format sln
    echo "Solution '$SOLUTION_NAME.sln' created."
else
    echo "Solution '$SOLUTION_NAME.sln' already exists."
fi

# Create projects uses function below to handle checks

# Function to create project if not exists
create_project() {
    local TYPE=$1
    local NAME=$2
    if [ ! -d "$NAME" ]; then
        dotnet new $TYPE -n "$NAME" -o "$NAME" --framework net10.0
        # Remove default files
        rm -f "$NAME/Class1.cs" "$NAME/WeatherForecast.cs"
        echo "Project '$NAME' created."
    else
        echo "Project '$NAME' already exists."
    fi
}

# Create Projects using function to handle checks
create_project classlib "$SOLUTION_NAME.Core"
create_project classlib "$SOLUTION_NAME.Application"
create_project classlib "$SOLUTION_NAME.Infrastructure"
create_project webapi "$SOLUTION_NAME.API" --use-minimal-apis

# Remove default files (extra safety)
rm -f "$SOLUTION_NAME.Core/Class1.cs"
rm -f "$SOLUTION_NAME.Application/Class1.cs"
rm -f "$SOLUTION_NAME.Infrastructure/Class1.cs"
rm -f "$SOLUTION_NAME.API/WeatherForecast.cs"
rm -f "$SOLUTION_NAME.API/WeatherForecast.cs" 

# Add projects to solution
dotnet sln "$SOLUTION_NAME.sln" add "$SOLUTION_NAME.Core/$SOLUTION_NAME.Core.csproj"
dotnet sln "$SOLUTION_NAME.sln" add "$SOLUTION_NAME.Application/$SOLUTION_NAME.Application.csproj"
dotnet sln "$SOLUTION_NAME.sln" add "$SOLUTION_NAME.Infrastructure/$SOLUTION_NAME.Infrastructure.csproj"
dotnet sln "$SOLUTION_NAME.sln" add "$SOLUTION_NAME.API/$SOLUTION_NAME.API.csproj"

# Add references
# Application -> Core
dotnet add "$SOLUTION_NAME.Application/$SOLUTION_NAME.Application.csproj" reference "$SOLUTION_NAME.Core/$SOLUTION_NAME.Core.csproj"

# Infrastructure -> Core, Application
dotnet add "$SOLUTION_NAME.Infrastructure/$SOLUTION_NAME.Infrastructure.csproj" reference "$SOLUTION_NAME.Core/$SOLUTION_NAME.Core.csproj"
dotnet add "$SOLUTION_NAME.Infrastructure/$SOLUTION_NAME.Infrastructure.csproj" reference "$SOLUTION_NAME.Application/$SOLUTION_NAME.Application.csproj"

# API -> Application, Infrastructure
dotnet add "$SOLUTION_NAME.API/$SOLUTION_NAME.API.csproj" reference "$SOLUTION_NAME.Application/$SOLUTION_NAME.Application.csproj"
dotnet add "$SOLUTION_NAME.API/$SOLUTION_NAME.API.csproj" reference "$SOLUTION_NAME.Infrastructure/$SOLUTION_NAME.Infrastructure.csproj"

echo "Solution setup complete!"
