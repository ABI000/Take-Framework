while getopts r:t:u:p:path: o
do
  case "$o" in
    t) TAG=$OPTARG ;;
    r) REPOSITORY=$OPTARG ;;
    u) DOCKER_USERNAME=$OPTARG ;;
    p) DOCKER_PASSWORD=$OPTARG ;;
    path) PATH=$OPTARG ;;
  esac
done
echo "build"
cd ${PATH}
dotnet publish -c Release -o ./bin/Publish
cd bin/Publish
docker buildx build --platform linux/amd64 -t ${REPOSITORY}:${TAG} . --no-cache
echo "login"
az acr login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD -n ${REPOSITORY}
echo "push"
docker push ${REPOSITORY}:${TAG}
echo "logout"
docker logout
