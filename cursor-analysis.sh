#!/bin/bash
# Install Cursor AI CLI if not already installed
npm install -g cursor-ai-cli

# Run analysis on code
cursor-ai analyze --fix-issues --output-format=json --output=cursor-report.json
