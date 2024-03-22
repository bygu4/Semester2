for f in $(find .. -name "*.sln"); do (
    dotnet build $f --nologo -clp:NoSummary  -v:q
    if [ $? -eq 0 ]; then
        dotnet test $f --no-build --nologo -clp:NoSummary -v:q
        if [ $? -eq 0 ]; then
            echo - ${f##*/}: test passed
        else
            echo - ${f##*/}: test failed
        fi
    else
        echo - ${f##*/}: build failed
    fi
) done
