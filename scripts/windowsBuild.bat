@echo off

cd ..

for /r %%i in (*.sln) do (
    dotnet build %%i --nologo -clp:NoSummary -v:q
    if %errorlevel% equ 0 (
        dotnet test %%i --no-build --nologo -v:q
        if %errorlevel% equ 0 (
            echo - %%~nxi: test passed
        ) else (
            echo - %%~nxi: test failed
            exit 1
        )
    ) else (
        echo - %%~nxi: build failed
        exit 1
    )
)
