#! /bin/bash
set -e   # quit on errors
set -x   # show step by step

### This script is intended to be run /locally/, and it should deploy tower on the remote machine
ssh droplet "cd /var/www/towerRedo/ && git pull"
ssh droplet "/var/www/towerRedo/deploy/manual_deploy.bash"

