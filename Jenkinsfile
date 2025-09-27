pipeline {
    agent any

    environment {
        DOTNET_PATH = 'C:\\Program Files\\dotnet\\dotnet.exe'
        WEBAPP_UI_PATH = 'C:\\Users\\samar\\source\\repos\\webapp-ui'
        BACKEND_PATH = 'C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2'
        WWWROOT_PATH = "${BACKEND_PATH}\\WebApplication2\\wwwroot"
        SOLUTION_FILE = "${BACKEND_PATH}\\WebApplication2.sln"
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
                echo "Restoring .NET packages..."
                bat "\"${DOTNET_PATH}\" restore \"${SOLUTION_FILE}\""

                echo "Building backend..."
                bat "\"${DOTNET_PATH}\" build \"${SOLUTION_FILE}\" --no-restore"
            }
        }

        stage('Run Unit Tests') {
            steps {
                dir(BACKEND_PATH) {
                    echo "Running backend unit tests..."
                    bat "\"${DOTNET_PATH}\" test \"${SOLUTION_FILE}\" --no-build --logger trx"
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
                echo "Deploying Angular frontend..."

                echo "Preparing wwwroot..."
                bat """
                if exist "${WWWROOT_PATH}" (
                    rmdir /s /q "${WWWROOT_PATH}"
                )
                mkdir "${WWWROOT_PATH}"
                """

                echo "Copying Angular build to wwwroot..."
                bat """
                if exist "${WEBAPP_UI_PATH}\\dist\\webapp-ui" (
                    robocopy "${WEBAPP_UI_PATH}\\dist\\webapp-ui" "${WWWROOT_PATH}" /E /MT:8 /IS
                    if %ERRORLEVEL% LSS 8 exit 0
                ) else (
                    echo "ERROR: Angular dist folder does not exist!"
                    exit /b 1
                )
                """
            }
        }

        stage('Run Backend') {
            steps {
                dir(BACKEND_PATH) {
                    echo "Stopping any running backend..."
                    bat '''
                    taskkill /IM WebApplication2.exe /F 2>NUL || echo "No running instance"
                     exit /b 0
                    '''

                    echo "Starting backend..."
                    bat "start \"Backend\" \"${DOTNET_PATH}\" run --project \"${SOLUTION_FILE}\""
                }
            }
        }

         stage('Code Quality - SonarQube (Skipped)') {
            steps {
                echo "Skipping SonarQube scan for this submission."
            }
        }

        stage('Monitoring (Placeholder)') {
            steps {
                echo "Monitoring stage configured (placeholder for HD)."
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
