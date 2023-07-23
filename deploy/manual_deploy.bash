#! /bin/bash
set -e
set -x

SCRIPTPATH="$( cd -- "$(dirname "$0")" >/dev/null 2>&1 ; pwd -P )"

# Go to top level directory
cd ${SCRIPTPATH}/..

# Grab the latest changes
git pull

# rebuild client
cd towerRedo.client
npm run build

# rebuild server
cd ../towerRedo
dotnet build
dotnet publish --configuration Release

# redo systemd configuration
cd ../deploy
cp towerRedo.service /etc/systemd/system/towerRedo.service
systemctl daemon-reload
systemctl restart towerRedo.service

# Not sure I really want to manually update nginx, not sure how that will play
# with certbot automatically renewing our nginx certificates.
#cp nginx.jeanneallen.us /etc/nginx/sites-available/jeanneallen.us

