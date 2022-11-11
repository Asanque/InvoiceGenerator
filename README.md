<div align="center">
  
# Számlázó
  
</div>


### Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Screenshots](#screenshots)
- [Install](#install)


# Introduction

This is an application to turn a database containing daily data into a format that makes creating monthly invoices easier.


# Features

- Written in C# using ASP.NET and razor pages.
- Data is in multiple excel spreadsheets, which get converted with VBA (Visual Basic for Applications).
- The data gets converted again into in memory database and output into printable html.
- The location of the excel file can be set using a file called "DocLocation.txt".
- The data is dynamicly converted, does not leave space if the month is only 4 weeks long

# Screenshots
sample data:
![App Screenshot](https://github.com/Asanque/Szamlazo/blob/main/SamplePictures/1NBRhJk.png)
converted into monthly data:

![App Screenshot](https://github.com/Asanque/Szamlazo/blob/main/SamplePictures/5QQHZe4.png)

Link to selected shop's page in browser:

![App Screenshot](https://github.com/Asanque/Szamlazo/blob/main/SamplePictures/QxzWKKU.png)

Sample page with 1 week:

![App Screenshot](https://github.com/Asanque/Szamlazo/blob/main/SamplePictures/3l4A17B.png)

Sample page with 5 week:

![App Screenshot](https://github.com/Asanque/Szamlazo/blob/main/SamplePictures/VZyGvjs.png)

# Install
[Download from here](https://drive.google.com/file/d/1EaieZVjUJvVoLZf7epdev7XU-JlQZ4-G/view?usp=share_link)

After downloaded, extract and run start.bat.
