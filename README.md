# masm_64_error_translate

example for NppExec
cls

set masm64=C:\masm64

cd "$(CURRENT_DIRECTORY)"

cmd /c if exist errors.txt del errors.txt

cmd /c if exist $(NAME_PART).obj del $(NAME_PART).obj

cmd /c if exist $(NAME_PART).exe del $(NAME_PART).exe

//compile $(masm64)\bin\ml64 >> errors.txt ...

if $(EXITCODE) !=0 goto exit

// link $(masm64)\bin\link ...

if $(EXITCODE) !=0 goto exit

cmd /c $(NAME_PART).exe

cmd /c del errors.txt

cmd /c del $(NAME_PART).obj

exit

:exit

cmd /c $(masm64)\bin64\masm_64_error_translate.exe errors.txt

con_loadfrom errors.txt
