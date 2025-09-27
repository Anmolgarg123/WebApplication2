pipeline {
    agent any

    environment {
        DOTNET_PATH = '"C:\\Program Files\\dotnet\\dotnet.exe"'
        NODE_PATH = '"C:\\Program Files\\nodejs"'
        PATH = "${NODE_PATH};C:\\Users\\samar\\.dotnet\\tools;${env.PATH}"
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
                dir('C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2') {
                    bat '"C:\\Program Files\\dotnet\\dotnet.exe" build WebApplication2.sln -c Release'
                }
            }
        }

        stage('Run .NET Tests') {
            steps {
                dir('C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2.Tests') {
                    bat '"C:\\Program Files\\dotnet\\dotnet.exe" test WebApplication2.Tests.csproj --logger "trx;LogFileName=TestResults.trx" -l "console;verbosity=detailed"'
                }
            }
        }

        stage('Convert TRX to JUnit XML') {
            steps {
                dir('C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2.Tests') {
                    // Ensure TRX_Flat exists
                    bat 'if not exist TRX_Flat mkdir TRX_Flat'

                    // Copy the single TestResults.trx to TRX_Flat
                    bat 'copy /Y TestResults\\TestResults.trx TRX_Flat\\TestResults.trx'

                    // Convert the TRX file to JUnit XML
                    bat '"C:\\Users\\samar\\.dotnet\\tools\\trx2junit.exe" "TRX_Flat\\TestResults.trx"'
                }
            }
        }

        stage('Publish Test Results') {
            steps {
                dir('C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2.Tests') {
                    junit 'TRX_Flat\\*.xml'
                }
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Running code quality checks...'
                dir('C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2') {
                    bat '"C:\\Program Files\\dotnet\\dotnet.exe" tool restore --verbosity minimal'
                    bat '"C:\\Program Files\\dotnet\\dotnet.exe" tool run dotnet-format WebApplication2.sln'
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
                // Delete old wwwroot if it exists
                bat 'if exist "C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2\\wwwroot" rmdir /s /q "C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2\\wwwroot"'

                // Copy new Angular build
                bat 'robocopy "C:\\Users\\samar\\source\\repos\\webapp-ui\\dist\\webapp-ui" "C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2\\wwwroot" /E /NFL /NDL /NJH /NJS /NC /NS /NP'
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
