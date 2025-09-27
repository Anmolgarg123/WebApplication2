pipeline {
    agent any

    environment {
        DOTNET_PATH = '"C:\\Program Files\\dotnet\\dotnet.exe"'
        NODE_PATH = "C:\\Program Files\\nodejs"
        PATH = "${NODE_PATH};C:\\Users\\samar\\.dotnet\\tools;${env.PATH}"
        API_PATH = "C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2"
        UI_PATH = "C:\\Users\\samar\\source\\repos\\webapp-ui"
    }

    stages {
        stage('Checkout SCM') {
            steps {
                checkout scm
            }
        }

        stage('Debug Environment') {
            steps {
                echo "Debugging NodeJS and .NET environment..."
                bat 'node -v'
                bat 'npm -v'
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" --version'
            }
        }

        stage('Build .NET API') {
            steps {
                dir("${API_PATH}") {
                    bat "${DOTNET_PATH} build WebApplication2.sln -c Release"
                }
            }
        }

        stage('Prepare TestResults Folder') {
            steps {
                dir("${API_PATH}") {
                    bat 'if not exist "WebApplication2.Tests\\TestResults" mkdir "WebApplication2.Tests\\TestResults"'
                }
            }
        }

        stage('Run .NET Tests') {
            steps {
                dir("${API_PATH}") {
                    bat '"C:\\Program Files\\dotnet\\dotnet.exe" test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger "trx;LogFileName=WebApplication2.Tests\\TestResults\\TestResults.trx" -l "console;verbosity=detailed"'
                }
            }
        }

        stage('Convert TRX to JUnit') {
            steps {
                dir("${API_PATH}") {
                    bat '"C:\\Users\\samar\\.dotnet\\tools\\trx2junit.exe" WebApplication2.Tests\\TestResults\\TestResults.trx'
                }
            }
        }

        stage('Publish Test Results') {
            steps {
                dir("${API_PATH}") {
                    junit 'WebApplication2.Tests\\TestResults\\TestResults.xml'
                }
            }
        }

        stage('Code Quality') {
            steps {
                dir("${API_PATH}") {
                    echo 'Running code quality checks...'
                    bat "${DOTNET_PATH} tool restore --verbosity minimal"
                    bat "${DOTNET_PATH} tool run dotnet-format WebApplication2.sln"
                }
            }
        }

        stage('Build Angular UI') {
            steps {
                dir("${UI_PATH}") {
                    bat 'npm install'
                    bat 'npm run build'
                }
            }
        }

        stage('Copy Angular UI to API') {
            steps {
                bat """
                if exist "${API_PATH}\\WebApplication2\\wwwroot" rmdir /s /q "${API_PATH}\\WebApplication2\\wwwroot"
                robocopy "${UI_PATH}\\dist\\webapp-ui" "${API_PATH}\\WebApplication2\\wwwroot" /E /NFL /NDL /NJH /NJS /NC /NS /NP || exit 0
                """
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
