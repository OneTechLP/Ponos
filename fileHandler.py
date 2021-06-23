# File contains a list of helpers for handling basic file management to creating the report
import os
import tkinter as tk
import csv

# Gets all files in directory
def getAllFilesInDir(directoryPath):
  files = []
  for (dirpath, dirnames, filenames) in os.walk(directoryPath):
    for file in filenames:
      files.append(os.path.join(dirpath, file))

  return files

# takes list of file paths and finds csv file. If more/less than 1, show error to user. Else we parse file. If no products, throw error,
# else returned parsed content
def checkAndValidatePricesCSV(files):
  matching = [filename for filename in files if ".csv" in filename]

  if len(matching) == 0:
    tk.messagebox.showerror(title="Invalid Folder", message="Could not find product and pricing csv file")
    return False
  elif len(matching) > 1:
    tk.messagebox.showerror(title="Invalid Folder", message="Too many csv files found. Expecting only one")
    return False

  filePath = matching[0]
    
  products = {}
  with open(filePath, newline='') as csvfile:
    reader = csv.DictReader(csvfile, ['productCode', 'price'])
    for row in reader:
      products[row['productCode']] = float(row['price'])
  
   # if empty products, exit
  if not products:
    tk.messagebox.showerror(title="Invalid Products list", message="Products list is empty")
    return False

  return {
    "products": products,
    "filePath": filePath
  }

# takes directory path and finds/validates prices csv file and returns receipt files and prices csv content
def handleFiles(directoryPath):
  files = getAllFilesInDir(directoryPath)
  
  productOutput = checkAndValidatePricesCSV(files)

  if not productOutput:
    return False
  
  files.remove(productOutput['filePath'])
  
  return {
    "files": files,
    "products": productOutput['products']
  }

def createMischargesCSV(fileOutput, mischarges):
  with fileOutput as csvfile:
    writer = csv.DictWriter(csvfile, ['productCode', 'count', 'total'])
    writer.writerow({'productCode': 'Product Code', 'count': 'Count', 'total': 'Total' })
    for item in mischarges:
      writer.writerow(item)
  fileOutput.close()