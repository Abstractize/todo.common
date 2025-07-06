#!/bin/bash

SRC_DIR="src"
OUTPUT_DIR="../.nupkgs"

mkdir -p "$OUTPUT_DIR"

find "$SRC_DIR" -maxdepth 2 -name "*.csproj" | while read -r csproj; do
    dotnet pack "$csproj" --configuration Release --output "$OUTPUT_DIR"
done