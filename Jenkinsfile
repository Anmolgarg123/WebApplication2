pipeline {
    agent any

    environment {
        DOTNET_PATH = '"C:\\Program Files\\dotnet\\dotnet.exe"'
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/Anmolgarg123/WebApplication2.git'
            }
        }

        stage('Build') {
            steps {
                bat "${DOTNET_PATH} build WebApplication2.sln -c Release"
            }
        }

        stage('Test') {
            steps {
                // Run tests and generate TRX (or JUnit XML) reports
                bat "${DOTNET_PATH} test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger \"trx;LogFileName=TestResults.trx\""
            }
            post {
                always {
                    // Publish test results to Jenkins
                    junit '**/TestResults.trx'
                }
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Code Quality stage: Integrate SonarQube or other tools here'
                // Example:
                // bat "sonar-scanner.bat -Dsonar.projectKey=WebApplication2 -Dsonar.sources=."
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploy stage: Add Docker or Azure/AWS deploy commands here'
                // Example:
                // bat "docker build -t webapp2:latest ."
                // bat "docker run -d -p 5000:80 webapp2:latest"
            }
        }
    }

    post {
        success {
            echo 'Pipeline completed successfully!'
        }
        failure {
            echo 'Pipeline failed. Check logs!'
        }
    }
}
