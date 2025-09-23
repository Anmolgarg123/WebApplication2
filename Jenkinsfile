pipeline {
    agent any

    environment {
        DOTNET_PATH = "C:\\Program Files\\dotnet\\dotnet.exe"  // adjust path if needed
    }

    stages {
        stage('Build') {
            steps {
                bat '"%DOTNET_PATH%" build WebApplication2.sln -c Release'
            }
        }

        stage('Test') {
            steps {
                bat '"%DOTNET_PATH%" test WebApplication2.Tests\\WebApplication2.Tests.csproj'
            }
            post {
                always {
                    junit '**/TestResults/*.xml'  // xUnit test report
                }
            }
        }

        stage('Code Quality') {
            steps {
                echo 'SonarQube analysis would go here'
                // You can configure SonarQube scanner in Jenkins
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploy to staging environment or Docker container'
                // Example: docker build & docker run commands
            }
        }
    }
    
    post {
        always {
            echo 'Pipeline finished!'
        }
    }
}
