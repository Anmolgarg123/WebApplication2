pipeline {
    agent any

    environment {
        DOTNET_PATH = '"C:\\Program Files\\dotnet\\dotnet.exe"'
        PATH = "C:\\Program Files\\nodejs;C:\\Users\\samar\\.dotnet\\tools;${env.PATH}"
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
                bat "${DOTNET_PATH} --version"
            }
        }

        stage('Build .NET API') {
            steps {
                dir('C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2') {
                    bat "${DOTNET_PATH} build WebApplication2.sln -c Release"
                }
            }
        }

        stage('Run .NET Tests') {
            steps {
                dir('C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2.Tests') {
                    bat "${DOTNET_PATH} test WebApplication2.Tests.csproj --logger \"trx;LogFileName=TestResults.trx\" --results-directory TestResults"
                }
            }
        }

        stage('Convert TRX to JUnit') {
    steps {
        dir('C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2.Tests') {
            // Copy any .trx files to a flat folder
            bat 'mkdir TRX_Flat || exit 0'
            bat 'copy TestResults\\**\\*.trx TRX_Flat\\'

            // Convert each .trx to JUnit
            bat 'for %f in (TRX_Flat\\*.trx) do "C:\\Users\\samar\\.dotnet\\tools\\trx2junit.exe" "%f"'
        }
    }
}


        stage('Publish Test Results') {
            steps {
                dir('C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2.Tests') {
                    junit 'TestResults\\TestResults.xml'
                }
            }
        }

        stage('Build Angular UI') {
            steps {
                dir('C:\\Users\\samar\\source\\repos\\webapp-ui') {
                    bat 'npm install'
                    bat 'npm run build'
                }
            }
        }

        stage('Copy Angular UI to API') {
            steps {
                bat '''
                if exist "C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2\\wwwroot" rmdir /s /q "C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2\\wwwroot"
                robocopy "C:\\Users\\samar\\source\\repos\\webapp-ui\\dist\\webapp-ui" "C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2\\wwwroot" /E /NFL /NDL /NJH /NJS /NC /NS /NP
                '''
            }
        }

        stage('Deploy') {
            steps {
                echo 'Add deployment steps here (IIS, Azure, Docker, etc.)'
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
