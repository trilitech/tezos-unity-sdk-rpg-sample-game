


# Tezos SDK For Unity - RPG Sample Game

This **Unity Project** demonstrates key features of the Tezos SDK For Unity within an RPG Sample Game.

### Getting Started
1. Download this repo (*.zip or *.git)
1. Download the [Unity Editor](https://store.unity.com/#plans-individual)
1. Open this repo in the Unity Editor
1. Enjoy!

### Documentation
* <a href="https://opentezos.com/gaming/unity-sdk">Web Documentation</a> - **Overview** of the "Tezos SDK For Unity"

### Configuration
* **Unity Project** - <a href="./Unity/">./Unity/</a>
* **Unity Version** - [Version](./Unity/ProjectSettings/ProjectVersion.txt)
* **Unity Target** - [Standalone MAC/PC](https://support.unity.com/hc/en-us/articles/206336795-What-platforms-are-supported-by-Unity-)
* **Unity Rendering** - [Universal Render Pipeline (URP)](https://docs.unity3d.com/Manual/universal-render-pipeline.html)
* **Unity Menus** - See `Unity → Window → Tezos → RPG Sample Game → Open ReadMe` for additional orientation
* **Unity Dependencies** - The [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui.html) resolves all project dependencies from the [Manifest.json](./Unity/Packages/manifest.json) including the <a href="https://github.com/trilitech/tezos-unity-sdk">Tezos SDK For Unity</a>. No further action is required


### Videos

<table>
<tr>
<th>Tezos SDK For Unity - Authentication</th>
<th>Tezos SDK For Unity - NFTS</th>
</tr>
<tr>
<td>
<a href="https://tbd/youtube/link"><img width="500" src="./Unity/Assets/Tezos/RPGSampleGame/Web3/Documentation/Images/YT_Thumbnail_Video_03.png" /></a>
</td>
<td>
<a href="https://tbd/youtube/link"><img width="500" src="./Unity/Assets/Tezos/RPGSampleGame/Web3/Documentation/Images/YT_Thumbnail_Video_04.png" /></a>
</td>
</tr>
</table>


### Screenshots

<table>
<tr>
<th>Scene01_IntroMenu</th>
<th>Scene02_Game</th>
</tr>
<tr>
<td>
<a href="./Unity/Assets/Tezos/RPGSampleGame/Web3/Documentation/Images/Scene01_IntroMenu.png"><img width="500" src="./Unity/Assets/Tezos/RPGSampleGame/Web3/Documentation/Images/Scene01_IntroMenu.png" /></a>
</td>
<td>
<a href="./Unity/Assets/Tezos/RPGSampleGame/Web3/Documentation/Images/Scene02_Game.png"><img width="500" src="./Unity/Assets/Tezos/RPGSampleGame/Web3/Documentation/Images/Scene02_Game.png" /></a>
</td>
</tr>
</table>

### Features

This project showcases several key features and use-cases for the "Tezos SDK For Unity". Here are highlights.

**Authentication**

Players connect to the blockchain with a Tezos-compatible mobile wallet. For this RPG sample game, authentication unlocks faster character movement.

To see this feature in action, play the `Scene01_IntroMenu` Scene. The <a href="./Unity/Assets/Tezos/RPGSampleGame/Web3/Scripts/Runtime/RPG/Scenes/Scene01_IntroMenu.cs">Scene01_IntroMenu.cs</a> class provides a full demonstration. Here is partial snippet.

```csharp
// Store reference for convenience
ITezosAPI tezos = TezosSingleton.Instance;

// Determines if the user is authenticated 
if (!tezos.HasActiveWalletAddress())
{
    // Makes a call to connect with a wallet
    tezos.ConnectWallet();
}
```

**NFTs**

Players check ownership of a given NFT. For this RPG sample game, NFT ownership unlocks secret walking pathways in the game world.

To see this feature in action, play the `Scene02_Game` Scene. The <a href="./Unity/Assets/Tezos/RPGSampleGame/Web3/Scripts/Runtime/RPG/Scenes/Scene02_Game.cs">Scene02_Game.cs</a> class provides a full demonstration. Here is partial snippet.
```csharp
// Setup
string demoNFTAddress = "KT1BRADdqGk2eLmMqvyWzqVmPQ1RCBCbW5dY";
int demoTokenId = 1;
            
// Store reference for convenience
ITezosAPI tezos = TezosSingleton.Instance;
        
// Returns the address of the current active wallet
string activeWalletAddress = tezos.GetActiveWalletAddress();

// Determines if the user account owns a given Nft
bool hasTheNft = tezos.IsOwnerOfToken(
    activeWalletAddress, 
    demoNFTAddress, 
    demoTokenId);

if (hasTheNft)
{
    // Unlock special game features
}
```

