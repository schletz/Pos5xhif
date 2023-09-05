#!/bin/bash

for dir in .vs .vscode bin obj
do
    find -name $dir -type d -exec rm -rf {} \;
done
