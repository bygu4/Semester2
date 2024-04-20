for f in $(find .. -name "*.sln"); do (
    dotnet build $f --nologo -clp:NoSummary  -v:m
    if [ $? -eq 0 ]; then
        dotnet test $f --no-build --nologo -v:m
        echo $?
        if [ $? -eq 0 ]; then
            echo - ${f##*/}: test passed
        else
            echo - ${f##*/}: test failed
            exit $?
        fi
    else
        echo - ${f##*/}: build failed
        exit 1
    fi
) done
