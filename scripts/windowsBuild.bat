@echo off

for /r %%i in (..\*.sln) do (
    dotnet build %%i --nologo -clp:NoSummary -v:q
    if errorlevel 0 (
        dotnet test %%i --no-build --nologo -clp:NoSummary -v:q
        if errorlevel 0 (
            echo - %%~nxi: test passed
        ) else (
            echo - %%~nxi: test failed
        )
    ) else (
        echo - %%~nxi: build failed
    )
)
