@echo off

for /l %%x in (1, 1, 25) do (
   echo %%x
   start ClientApp.exe
) 