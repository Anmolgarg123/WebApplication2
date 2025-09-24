pipeline {
    agent any

    environment {
        // Node.js path variable
        path1 = "C:\\Program Files\\nodejs"
        PATH = "${path1};${env.PATH}"
    }

    stages {
        stage('Checkout SCM') {
            steps {
                checkout scm
            }
        }

        stage('Build .NET API') {
            steps {
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" build WebApplication2.sln -c Release'
            }
        }

        stage('Run Tests') {
            steps {
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
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" tool restore --verbosity minimal'
                bat script: '"C:\\Program Files\\dotnet\\dotnet.exe" tool run dotnet-format WebApplication2.sln --check', returnStatus: true
            }
        }

        stage('Build Angular UI') {
            steps {
                dir('C:\\Users\\hp\\source\\repos\\webapp-ui') {
                    bat 'npm install'
                    bat 'npm run build -- --prod'
                }
            }
        }

        stage('Copy Angular to API') {
            steps {
                echo 'Copying Angular build to API wwwroot...'
                bat 'xcopy /E /Y /I "C:\\Users\\hp\\source\\repos\\webapp-ui\\dist\\webapp-ui\\*" "C:\\ProgramData\\Jenkins\\.jenkins\\workspace\\High Distinction Task\\WebApplication2\\wwwroot\\"'
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
