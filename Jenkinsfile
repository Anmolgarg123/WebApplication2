pipeline {
    agent any

    environment {
        DOTNET_PATH = 'C:\\Program Files\\dotnet\\dotnet.exe'
        WEBAPP_UI_PATH = 'C:\\Users\\samar\\source\\repos\\webapp-ui'
        SOLUTION_PATH = 'C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2.sln'
        TEST_PROJECT_PATH = 'C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2.Tests'
        BACKEND_PATH = 'C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2'
        WWWROOT_PATH = "${BACKEND_PATH}\\wwwroot"
    }

    stages {
        stage('Clean Workspace') {
            steps {
                echo "Cleaning workspace..."
                deleteDir()
            }
        }

        stage('Restore & Build Backend') {
            steps {
                dir("C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2") {
                    echo "Restoring .NET solution..."
                    bat "\"${DOTNET_PATH}\" restore WebApplication2.sln"

                    echo "Building backend solution..."
                    bat "\"${DOTNET_PATH}\" build WebApplication2.sln --no-restore"
                }
            }
        }

        stage('Run Unit Tests') {
            steps {
                dir("${TEST_PROJECT_PATH}") {
                    echo "Running backend tests..."
                    bat "\"${DOTNET_PATH}\" test --no-build --logger trx"
                }
            }
        }

        stage('Build Frontend') {
            steps {
                dir(WEBAPP_UI_PATH) {
                    echo "Installing Node packages..."
                    bat "npm install"

                    echo "Building Angular project..."
                    bat "npm run build"
                }
            }
        }

        stage('Deploy Frontend') {
            steps {
                echo "Copying Angular build to wwwroot..."
                // Remove old wwwroot safely (ignore if not exists)
                bat "if exist \"${WWWROOT_PATH}\" rmdir /s /q \"${WWWROOT_PATH}\""
                bat "robocopy \"${WEBAPP_UI_PATH}\\dist\\webapp-ui\" \"${WWWROOT_PATH}\" /E /NFL /NDL /NJH /NJS /NC /NS /NP"
            }
        }

        stage('Run Backend') {
            steps {
                dir(BACKEND_PATH) {
                    echo "Stopping any running backend..."
                    bat "taskkill /IM WebApplication2.exe /F || echo 'No running instance'"

                    echo "Starting backend..."
                    // Starts backend in background without blocking pipeline
                    bat "start \"Backend\" \"${DOTNET_PATH}\" run"
                }
            }
        }

        stage('Code Quality - SonarQube') {
            steps {
                echo "Running SonarQube scan..."
                // Example, configure your SonarQube server in Jenkins first
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
