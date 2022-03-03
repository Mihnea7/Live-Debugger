pipeline {
    agent any

    stages {
        stage ("Build") {
            steps
            {
                echo "Starting Build stage"
                sh '''
                    export PATH=/usr/share/dotnet:$PATH
                    export TMPDIR=/tmp/NuGetScratch
                    mkdir -p ${TMPDIR}
                    chmod +x build_project.sh
                    dotnet build sample_project
                    dotnet run --project sample_project/Program
                '''
            }
            post {
                failure {
                    echo "Build failed"
                }
                success {
                    echo "Build succeded"
                }
            }
        }

        stage ("Test") {
            steps {
                echo "Starting Test stage"
                sh '''
                    export PATH=/usr/share/dotnet:$PATH
                    chmod +x test_project.sh
                    ./test_project.sh
                '''
            }

            post {
                failure {
                    echo "Test failed"
                }
                success {
                    echo "Test succeded"
                }
            }
        }

    }
}
