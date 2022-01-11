# BTC Web Wallet

This is a Bitcoin Wallet Web User Interface depending on bitcoin core application and JsonRPC 1.0. This application can be used as a bitcoin node, hot or cold wallet purposes with a bootstrap powered nice web user interface. So this project is intended to create a good UEX for secure bitcoin cold and hot wallet.

## Installation

In this section there is commands to install this product to servers or desktop computers with several operationg systems.

### MacOS & Linux

#### Install [HomeBrew](https://brew.sh/)
```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```
#### Install [Bitcoin Core](https://bitcoin.org)
```bash
brew install bitcoin 
```
The default path of the daemon would be like */usr/local/opt/bitcoin/bin/bitcoind* 
If you would install **bitcoind** into another path you should also specify that in the [appSettings.json](appSettings.json). 
If you are using the default path for **bitcoind**, there is nothing to do more here.

[bitcoin.conf](bitcoin.conf) file is the config of the bitcoin node which will be stored in your computer. 
With default parameters it will install a prune (2GB) node, encryptwallets will run in main chain. 
If you want to do advanced stuff with it please read [bitcoid manual pages](bitcoind_manual.txt).

#### Install [DotNet Core](https://dotnet.microsoft.com/en-us/download/dotnet/scripts)
```bash
https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh
```
#### Install [Git](https://git-scm.com/)
```bash
brew install git
```

#### Install BTC Web Client
```bash
git clone https://github.com/ogu83/BTCWebWallet.git
cd BTCWebWallet
```

#### Start Web Client
Finally run following command and start your BTC Web Wallet in your computer.
```bash
dotnet restore
dotnet build
dotnet run
```
Goto https://localhost:9999

## Tech Stack
- bitcoin core
- dotnet core 6.0
- MVC Web App with Razor Pages.
- Jquery
- Bootstrap

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
