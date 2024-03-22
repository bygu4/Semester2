set errorcode=0

for f in $(find .. -name "*.sln"); do (
    dotnet build $f --nologo --consoleLoggerParameters:NoSummary -v:q
    if [ $? -eq 0 ]; then
        dotnet test $f --no-build --nologo --consoleLoggerParameters:NoSummary -v:q
        if [ $? -eq 0 ]; then
            echo - ${f##*/}: test passed
        else
            echo - ${f##*/}: test failed
            set $errorcode=1
        fi
    else
        echo - ${f##*/}: build failed
        set $errorcode=1
    fi
) done
exit $errorcode
