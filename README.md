
# Pokémon Console Application

This application allows you to retrieve information about Pokémon strengths and weaknesses against other types.

## Installation

To run the Pokémon console application locally, you need to have [.NET Core](https://dotnet.microsoft.com/download) installed on your machine.

1. Clone this repository to your local machine using the following command: git clone https://github.com/AnkitaGarg17/pokemon_application.git

2. Open a command prompt or terminal and navigate to the project directory.

3.Build the application: dotnet build

4. 4. Run the following command to restore the NuGet packages: dotnet restore

## Usage

1. Run the application: dotnet run
   
2. Enter the name of the Pokémon you want to get strengths and weaknesses for.

3. The application will display the strengths and weaknesses of the Pokémon against other types.

 #Example -
 
Enter pokemon name?
ditto
Pokemon ditto's Strengths:
ghost

Pokemon ditto's Weaknesses:
fighting
rock
steel
ghost

## Error Handling
If you encounter any errors or the application does not return any data for a Pokémon, ensure the following:

The Pokémon name is spelled correctly.
Your internet connection is stable.
The PokéAPI is available and accessible.

## Dependencies

This application utilizes the following dependencies:
- System.Text.Json - A popular JSON framework for .NET.

These dependencies are managed via NuGet and will be automatically restored during the `dotnet restore` step.

