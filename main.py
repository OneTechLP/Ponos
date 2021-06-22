import os
import tkinter as tk
import tkinter.font as tkFont
from tkinter import filedialog
from fileHandler import handleFiles, createMischargesCSV
from receiptAnalyzer import ReceiptAnalyzer

DEFAULT_FOLDER_SELECTED_LABEL_TEXT = "No folder selected"

class App:
    def __init__(self, root):
        root.title("Ponos")
        width=600
        height=500
        screenwidth = root.winfo_screenwidth()
        screenheight = root.winfo_screenheight()
        alignstr = '%dx%d+%d+%d' % (width, height, (screenwidth - width) / 2, (screenheight - height) / 2)
        root.geometry(alignstr)
        root.resizable(width=False, height=False)
        root.configure(bg='#333333')

        FolderSelectButton=tk.Button(root)
        FolderSelectButton["bg"] = "#efefef"
        ft = tkFont.Font(family='Times',size=14)
        FolderSelectButton["font"] = ft
        FolderSelectButton["fg"] = "#000000"
        FolderSelectButton["justify"] = "center"
        FolderSelectButton["text"] = "Select Folder to review"
        FolderSelectButton.place(x=20,y=30,width=225,height=25)
        FolderSelectButton["command"] = self.FolderSelectButton_command

        self.FolderSelectedText = tk.Label(root)
        self.FolderSelectedText["text"] = DEFAULT_FOLDER_SELECTED_LABEL_TEXT
        self.FolderSelectedText["anchor"] = "w"
        self.FolderSelectedText["wraplength"] = "550"
        self.FolderSelectedText.place(x=20,y=60,width=550,height=50)                            

        self.GenerateReportButton=tk.Button(root)
        self.GenerateReportButton["bg"] = "#efefef"
        ft = tkFont.Font(family='Times',size=14)
        self.GenerateReportButton["font"] = ft
        self.GenerateReportButton["fg"] = "#000000"
        self.GenerateReportButton["justify"] = "center"
        self.GenerateReportButton["text"] = "Generate Report"
        self.GenerateReportButton.place(x=20,y=120,width=125,height=25)
        self.GenerateReportButton["command"] = self.GenerateReportButton_command
        self.GenerateReportButton["state"] = "disabled"

    # opens file explorer in current directory
    # asks user for folder to generate report from
    def FolderSelectButton_command(self):
      self.folderName = filedialog.askdirectory(initialdir = os.getcwd())
      
      if self.folderName: 
        self.FolderSelectedText.configure(text = "Folder Opened: " + self.folderName)
        self.GenerateReportButton["state"] = "normal"
      else:
        self.FolderSelectedText.configure(text = DEFAULT_FOLDER_SELECTED_LABEL_TEXT)
        self.GenerateReportButton["state"] = "disabled"

    # takes folder and runs necessary commands to get list of mischarges
    # then asks user where to save generated csv file
    def GenerateReportButton_command(self):
      directoryPath = self.folderName
      # get 
      filesOutput = handleFiles(directoryPath)
      receiptChecker = ReceiptAnalyzer(filesOutput['products'])
      receipts = filesOutput['files']
      
      for receipt in receipts:
        receiptContent = receiptChecker.parseReceipt(receipt)
        receiptChecker.validatePrices(receiptContent)

      mischargesFile = filedialog.asksaveasfile(mode="w",filetypes=[("CSV Files","*.csv")])
      createMischargesCSV(mischargesFile, receiptChecker.mischarges)
      

if __name__ == "__main__":
    root = tk.Tk()
    app = App(root)
    root.mainloop()
