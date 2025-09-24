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
                // Run tests with TRX logger
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger "trx;LogFileName=TestResults.trx" -l "console;verbosity=detailed"'
            }
        }

        stage('Convert TRX to JUnit') {
            steps {
                bat '"C:\\Users\\hp\\.dotnet\\tools\\trx2junit.exe" WebApplication2.Tests\\TestResults\\TestResults.trx'
            }
        }

        stage('Publish Test Results') {
            steps {
                junit 'WebApplication2.Tests\\TestResults\\TestResults.xml'
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Running code quality checks...'

                // Restore repo-local dotnet tools
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" tool restore --verbosity minimal'

                // Run dotnet-format on the solution in check mode
                // If you want pipeline to fail on formatting errors, remove "returnStatus: true"
                bat script: '"C:\\Program Files\\dotnet\\dotnet.exe" tool run dotnet-format WebApplication2.sln --verify-no-changes', returnStatus: true
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
