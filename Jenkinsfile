pipeline {
    agent any

    environment {
        DOTNET_PATH = 'C:\\Program Files\\dotnet\\dotnet.exe'
    }

    stages {
        stage('Checkout Code') {
            steps {
                echo "Cloning repository into Jenkins workspace..."
                checkout scm
            }
        }

        stage('Restore & Build Backend') {
            steps {
                dir("WebApplication2/WebApplication2") {
                    echo "Restoring .NET packages..."
                    bat "\"${DOTNET_PATH}\" restore"

                    echo "Building backend..."
                    bat "\"${DOTNET_PATH}\" build --no-restore"
                }
            }
        }

        stage('Run Unit Tests') {
            steps {
                dir("WebApplication2/WebApplication2.Tests") {
                    echo "Running backend tests..."
                    bat "\"${DOTNET_PATH}\" test --no-build --logger trx"
                }
            }
        }

        stage('Build Frontend') {
            steps {
                dir("webapp-ui") {
                    echo "Installing Node packages..."
                    bat "npm install"

                    echo "Building Angular project..."
                    bat "npm run build"
                }
            }
        }

        stage('Deploy Frontend') {
            steps {
                echo "Copying Angular build to backend wwwroot..."
                bat """
                if exist "WebApplication2/WebApplication2/wwwroot" rmdir /s /q "WebApplication2/WebApplication2/wwwroot"
                mkdir "WebApplication2/WebApplication2/wwwroot"
                robocopy "webapp-ui/dist/webapp-ui" "WebApplication2/WebApplication2/wwwroot" /E /NFL /NDL /NJH /NJS /NC /NS /NP
                """
            }
        }

        stage('Run Backend') {
            steps {
                dir("WebApplication2/WebApplication2") {
                    echo "Starting backend..."
                    bat "start \"Backend\" \"${DOTNET_PATH}\" run"
                }
            }
        }

        stage('Code Quality - SonarQube') {
            steps {
                echo "Running SonarQube scan..."
                bat "sonar-scanner"
            }
        }
    }

    post {
        always {
            echo 'Pipeline finished.'
        }
        failure {
            echo 'Pipeline failed. Check logs.'
        }
    }
}
