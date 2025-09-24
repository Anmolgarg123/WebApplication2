pipeline {
    agent any

    stages {
        stage('Checkout SCM') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" build WebApplication2.sln -c Release'
            }
        }

       stage('Test') {
    steps {
        bat '"C:\\Program Files\\dotnet\\dotnet.exe" test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger "trx;LogFileName=TestResults.trx" -l "console;verbosity=detailed"'
    }
}


        stage('Convert TRX to JUnit') {
            steps {
                // Convert TRX to JUnit XML
                bat '"C:\\Users\\hp\\.dotnet\\tools\\trx2junit.exe" WebApplication2.Tests\\TestResults\\TestResults.trx'
            }
        }

        stage('Publish Test Results') {
            steps {
                // Publish the JUnit XML to Jenkins
                junit 'WebApplication2.Tests\\TestResults\\TestResults.xml'
            }
        }

      stage('Code Quality') {
        steps {
            echo 'Running code quality checks...'

            // Restore repo-local dotnet tools
            bat '"C:\\Program Files\\dotnet\\dotnet.exe" tool restore --verbosity minimal'

            // Run dotnet-format on the solution in check mode
            bat '"C:\\Program Files\\dotnet\\dotnet.exe" tool run dotnet-format WebApplication2.sln --verify-no-changes'
        }
    }



        stage('Deploy') {
            steps {
                echo 'Add deployment steps here'
            }
        }
    }

    post {
        always {
            echo 'Pipeline finished!'
        }
    }
}
