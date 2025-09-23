pipeline {
    agent any

    environment {
        DOTNET_PATH = '"C:\\Program Files\\dotnet\\dotnet.exe"'
        BUILD_CONFIGURATION = 'Release'
    }

    stages {

        // 1. Checkout code from GitHub
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/Anmolgarg123/WebApplication2.git'
            }
        }

        // 2. Build stage
        stage('Build') {
            steps {
                bat "${DOTNET_PATH} build WebApplication2.sln -c ${BUILD_CONFIGURATION}"
            }
        }

        // 3. Test stage
        stage('Test') {
            steps {
                // Run tests and generate JUnit XML report
                bat "${DOTNET_PATH} test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger \"junit;LogFilePath=TestResults.xml\""
            }
            post {
                always {
                    // Publish JUnit test results in Jenkins
                    junit '**/TestResults.xml'
                }
            }
        }

        // 4. Code Quality stage (SonarQube example)
        stage('Code Quality') {
            steps {
                // Optional: replace with your SonarQube project key and server
                bat "${DOTNET_PATH} sonarscanner begin /k:\"WebApplication2\" /d:sonar.login=\"YOUR_SONAR_TOKEN\""
                bat "${DOTNET_PATH} build WebApplication2.sln"
                bat "${DOTNET_PATH} sonarscanner end /d:sonar.login=\"YOUR_SONAR_TOKEN\""
            }
        }

        // 5. Deploy stage (example using local IIS or Docker)
        stage('Deploy') {
            steps {
                // Example: build Docker image and run container
                bat 'docker build -t webapplication2:latest .'
                bat 'docker stop webapplication2 || exit 0'
                bat 'docker rm webapplication2 || exit 0'
                bat 'docker run -d -p 5000:80 --name webapplication2 webapplication2:latest'
            }
        }
    }

    post {
        success {
            echo 'Pipeline completed successfully!'
        }
        failure {
            echo 'Pipeline failed. Check the logs!'
        }
    }
}
