#!/bin/bash
DOCKER_TAG=''

case "$TRAVIS_BRANCH" in
  "master")
    DOCKER_TAG=latest
    ;;
  "develop")
    DOCKER_TAG=dev
    ;;    
esac

docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
docker build -t npost.services.parcels:$DOCKER_TAG .
docker tag npost.services.parcels:$DOCKER_TAG $DOCKER_USERNAME/npost.services.parcels:$DOCKER_TAG
docker push $DOCKER_USERNAME/npost.services.parcels:$DOCKER_TAG