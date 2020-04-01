dotnet test --logger "trx;LogFileName=TestResults.trx" ^
            --logger "xunit;LogFileName=TestResults.xml" ^
            --results-directory ./BuildReports/UnitTests ^
            /p:CollectCoverage=true ^
            /p:CoverletOutput=BuildReports\Coverage\ ^
            /p:CoverletOutputFormat=cobertura ^
            /p:Exclude="[xunit.*]*

tools\reportgenerator.exe ^
       "-reports:BuildReports\Coverage\coverage.cobertura.xml" ^
       "-targetdir:BuildReports\Coverage" ^
       "-filefilters:-*.cshtml" ^
       "-classfilters:-*.CreateIdentitySchema;-*.AutoMapping;-*.ErrorViewModel;-*.Program;-*.Startup" ^
       "-title:MyReport" ^
       -reporttypes:HTML;HTMLSummary

start BuildReports\Coverage\index.htm