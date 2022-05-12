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
https://imgur.com/a/kmI61eP
![test](https://imgur.com/1NBRhJk)


# Install
``` sh
git clone git@github.com:Asanque/Szamlazo.git
```

After the repository is successfully cloned 
