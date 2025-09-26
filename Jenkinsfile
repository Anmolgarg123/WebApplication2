pipeline {
    agent any

    environment {
        DOTNET_PATH = '"C:\\Program Files\\dotnet\\dotnet.exe"'
        NODE_PATH = "C:\\Program Files\\nodejs"
        PATH = "${NODE_PATH};C:\\Users\\samar\\.dotnet\\tools;${env.PATH}"
        DOTNET_SOLUTION = "C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2.sln"
        DOTNET_TEST = "C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2.Tests\\WebApplication2.Tests.csproj"
        ANGULAR_PATH = "C:\\Users\\samar\\source\\repos\\webapp-ui"
        ANGULAR_DIST = "${ANGULAR_PATH}\\dist\\webapp-ui"
        DOTNET_WWWROOT = "C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2\\wwwroot"
    }

    stages {

        stage('Checkout SCM') {
            steps {
                checkout scm
            }
        }

        stage('Debug Environment') {
            steps {
                echo "NodeJS version:"
                bat 'node -v'
                echo "NPM version:"
                bat 'npm -v'
                echo ".NET version:"
                bat "${DOTNET_PATH} --version"
            }
        }

        stage('Build .NET API') {
            steps {
                echo "Building .NET Solution..."
                bat "${DOTNET_PATH} build \"${DOTNET_SOLUTION}\" -c Release"
            }
        }

        stage('Run .NET Tests') {
            steps {
                echo "Running .NET Tests..."
                bat "${DOTNET_PATH} test \"${DOTNET_TEST}\" --logger \"trx;LogFileName=TestResults.trx\""
            }
        }

        stage('Build Angular UI') {
            steps {
                dir("${ANGULAR_PATH}") {
                    echo "Installing Angular dependencies..."
                    bat 'npm install'
                    echo "Building Angular app..."
                    bat 'npm run build'
                }
            }
        }

        stage('Copy Angular UI to API wwwroot') {
            steps {
                echo "Copying Angular build to .NET wwwroot..."
                bat """
                if exist "${DOTNET_WWWROOT}" rmdir /s /q "${DOTNET_WWWROOT}"
                robocopy "${ANGULAR_DIST}" "${DOTNET_WWWROOT}" /E /NFL /NDL /NJH /NJS /NC /NS /NP
                """
            }
        }

        stage('Deploy (Placeholder)') {
            steps {
                echo "Add your deployment steps here (IIS, Azure, Docker, etc.)"
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
