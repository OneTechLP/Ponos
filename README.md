# Ponos
The problem statement is defined in the gistfile.  The data upon which your application will operate on exists in the tar.gz file.  Reach out to your hiring manager for any questions regarding the problem.  

# How to run
1. Install python (https://www.python.org/downloads/) *Make sure to check options for adding to path/environment variables and installing tk (tkinter)
2. Command to run app:
   - (Windows) run `python.exe main.py` inside project directory
   - (MacOS/Linux) run `python3 main.py` inside project directory
3. When extracting `tar` files on Windows, be sure to remove `PaxHeader` folders from output.
    - Program is expecting a single CSV file for the prices
    - Run this powershell command inside the root of the extracted `tar` file to remove them: 
      - `get-childitem -Include "PaxHeader" -Recurse -force | Remove-Item -Force -Recurse`
