#! /bin/sh

RED='\033[0;31m'
ORANGE='\033[0;33m'
NC='\033[0m' # No Color
printf "${NC}---------------------------------------------------------------------------------------------------------------\n"
printf "${RED}TRAVIS BUILD SHELL AUTOSCRIPT"
printf "${ORANGE}Please wait until everything is built"
printf "${NC}---------------------------------------------------------------------------------------------------------------"
echo -e "\n\n"
echo "get a nice cat in the meantime:\n"

cat << "DRAW"

 /\     /\
  {  `---'  }
  {  O   O  }
  ~~>  V  <~~
   \  \|/  /
    `-----'__
    /     \  `^\_
   {       }\ |\_\_   W
   |  \_/  |/ /  \_\_( )
    \__/  /(_E     \__/
      (  /
       MM


DRAW


echo 'Building the VB app'
xbuild /p:Configuration=Release MinecraftInstallerPP.sln
echo 'All done! bye'
