pipeline {
    agent any

    environment {
        DOTNET_PATH = '"C:\\Program Files\\dotnet\\dotnet.exe"'
        NODEJS_HOME = tool name: 'NodeJS 20', type: 'NodeJS'
        PATH = "${env.NODEJS_HOME}\\bin;C:\\Program Files\\dotnet;${env.PATH}"
    }

    stages {
        stage('Checkout SCM') {
            steps {
                checkout scm
            }
        }

        stage('Build .NET API') {
            steps {
                bat "${DOTNET_PATH} build WebApplication2.sln -c Release"
            }
        }

        stage('Run Tests') {
            steps {
                bat "${DOTNET_PATH} test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger \"trx;LogFileName=TestResults.trx\" -l \"console;verbosity=detailed\""
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
                bat "${DOTNET_PATH} tool restore --verbosity minimal"
                bat script: "${DOTNET_PATH} tool run dotnet-format WebApplication2.sln --check", returnStatus: true
            }
        }

        stage('Build Angular UI') {
            steps {
                dir('C:\\Users\\hp\\source\\repos\\webapp-ui') {
                    bat 'npm install'
                    bat 'npm run build'
                }
            }
        }

        stage('Copy Angular UI to API') {
            steps {
                powershell '''
                    $src = "C:\\Users\\hp\\source\\repos\\webapp-ui\\dist\\webapp-ui"
                    $dest = "WebApplication2\\wwwroot"
                    if (Test-Path $dest) { Remove-Item -Recurse -Force $dest }
                    Copy-Item -Recurse -Force $src $dest
                '''
            }
        }

        stage('Deploy') {
            steps {
                echo 'Add deployment steps here (e.g., IIS, Azure, Docker, etc.)'
            }
        }
    }

    post {
        always {
            echo 'Pipeline finished!'
        }
        success {
            echo 'Pipeline completed successfully!'
        }
        failure {
            echo 'Pipeline failed. Check logs for errors.'
        }
    }
}
