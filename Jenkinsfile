pipeline {
    agent any

    environment {
        DOTNET_PATH = 'C:\\Program Files\\dotnet\\dotnet.exe'
        WEBAPP_UI_PATH = 'C:\\Users\\samar\\source\\repos\\webapp-ui'
        BACKEND_PATH = 'C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2'
        WWWROOT_PATH = "${BACKEND_PATH}\\WebApplication2\\wwwroot"
        SOLUTION_FILE = "${BACKEND_PATH}\\WebApplication2.sln"
        SONAR_TOKEN = 'squ_5cbdf924e31a93210eb626a9206aba223942c0d5'
        SONAR_PROJECT_KEY = 'WebApplication2'
        SONAR_HOST_URL = 'http://localhost:9000'
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

            echo "Starting backend in background..."
            // 'start' runs the API in a new window, so pipeline continues
            bat "start \"Backend\" \"${DOTNET_PATH}\" run --project \"${SOLUTION_FILE}\""
        }
    }
}


        stage('Code Quality - SonarQube') {
            steps {
                echo "Running SonarQube scan..."
                bat "\"C:\\Users\\samar\\.dotnet\\tools\\dotnet-sonarscanner.exe\" begin /k:\"${SONAR_PROJECT_KEY}\" /d:sonar.login=\"${SONAR_TOKEN}\" /d:sonar.host.url=\"${SONAR_HOST_URL}\""

                bat "\"${DOTNET_PATH}\" build \"${SOLUTION_FILE}\""

                bat "\"C:\\Users\\samar\\.dotnet\\tools\\dotnet-sonarscanner.exe\" end /d:sonar.login=\"${SONAR_TOKEN}\""
            }
        }

        stage('Security Scan') {
            steps {
                dir(BACKEND_PATH) {
                    echo "Running security scan for vulnerable dependencies..."
                    bat "\"${DOTNET_PATH}\" list package --vulnerable"
                }
            }
        }
    } // closes stages

    post {
        always {
            echo 'Pipeline finished.'
        }
        failure {
            echo 'Pipeline failed. Check logs.'
        }
    }
} // closes pipeline
