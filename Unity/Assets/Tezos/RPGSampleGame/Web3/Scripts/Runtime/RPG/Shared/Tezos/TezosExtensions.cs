using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Cysharp.Threading.Tasks;
using Scripts.Tezos;
using UnityEngine;
using UnityEngine.Networking;

namespace TezosSDKExamples.Shared.Tezos
{
    /// <summary>
    /// Helper methods
    /// </summary>
    public static class TezosExtensions
    {
        /// <summary>
        /// Determines if the user is authenticated with
        /// the Tezos SDK For Unity
        /// </summary>
        public static bool HasActiveWalletAddress(this ITezos tezos)
        {
            return !string.IsNullOrEmpty(tezos.Wallet.GetActiveAddress());
        }

        /// <summary>
        /// Determines if the authenticated user account owns a given token
        /// </summary>
        public static async UniTask<bool> IsOwnerOfToken(this ITezos tezos, string account, string contract, int tokenId)
        {
            return await CheckTokenBalance(account, contract, tokenId);
        }

        /// <summary>
        /// Gets all tokens owned by the authenticated user account
        /// </summary>
        /// <param name="account"></param>
        public static async UniTask<List<TezosSDKExamples.Shared.Tezos.TokenBalance>> GetAllTokensForOwner(this ITezos tezos, string account)
        {
            return await GetAllTokens(account);
        }

        /// <summary>
        /// Gets token balance for the authenticated user account
        /// </summary>
        private static async UniTask<bool> CheckTokenBalance(string account, string contract, int tokenId)
        {
            string BaseUrl = "https://api.tzkt.io/v1/tokens/balances?balance.ne=0";
            string url = $"{BaseUrl}&account={account}&token.contract={contract}&token.tokenId={tokenId}&select=id";

            bool isOwner = false;
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogError("Error: " + request.error);
                }
                else
                {
                    //Debug.Log("Response: " + request.downloadHandler.text);
                    isOwner = JsonHelper.FromJson<int>(request.downloadHandler.text).Length > 0;
                    //Debug.Log($"Account {account} ownership status for token {contract}#{tokenId}: " + isOwner);
                }
            }

            return isOwner;
        }

        
        /// <summary>
        /// Gets token balances for the authenticated user account
        /// </summary>
        private static async UniTask<List<TezosSDKExamples.Shared.Tezos.TokenBalance>> GetAllTokens(string account)
        {
            string BaseUrl = "https://api.tzkt.io/v1/tokens/balances?balance.ne=0";
            string url =
                $"{BaseUrl}&account={account}&select=account.address%20as%20owner,balance,token.contract.address%20as%20tokenContract,token.tokenId%20as%20tokenId,token.metadata%20as%20tokenMetadata,lastTime,id";
            List<TezosSDKExamples.Shared.Tezos.TokenBalance> tokenBalances = new List<TezosSDKExamples.Shared.Tezos.TokenBalance>();
            
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogError("Error: " + request.error);
                }
                else
                {
                    //Debug.Log("Response: " + request.downloadHandler.text);
                    tokenBalances = JsonHelper.FromJson<TezosSDKExamples.Shared.Tezos.TokenBalance>(request.downloadHandler.text).ToList();
                    // foreach (TokenBalance tokenBalance in tokenBalances)
                    // {
                    //     Debug.Log(
                    //         $"{tokenBalance.tokenContract}#{tokenBalance.tokenId} => {tokenBalance.balance} (last updated {tokenBalance.lastTime})");
                    // }
                }
            }

            return tokenBalances;
        }


        /// <summary>
        /// Helper class for deserializing JSON arrays
        /// </summary>
        public static class JsonHelper
        {
            public static T[] FromJson<T>(string json_array)
            {
                string json_obj = "{\"items\":" + json_array + "}";
                Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json_obj);
                return wrapper.items;
            }

            [Serializable]
            private class Wrapper<T>
            {
                public T[] items;
            }
        }
    }
    
    /// <summary>
    /// Token balance data structure
    /// </summary>
    [Serializable]
    public class TokenBalance
    {
        /// <summary>
        /// Internal TzKT id.  
        /// **[sortable]**
        /// </summary>
        public long id;

        /// <summary>
        /// Owner account.  
        /// Click on the field to expand more details.
        /// </summary>
        public string owner;

        /// <summary>
        /// Balance (raw value, not divided by `decimals`).  
        /// **[sortable]**
        /// </summary>
        public string balance;

        /// <summary>
        /// Contract, created the token.
        /// </summary>
        public string tokenContract;

        /// <summary>
        /// Token id, unique within the contract.
        /// </summary>
        public string tokenId;

        /// <summary>
        /// Token metadata.
        /// </summary>
        public JsonElement tokenMetadata;

        /// <summary>
        /// Timestamp of the block where the token balance was last changed.
        /// </summary>
        public string lastTime; // JsonUtility is bad at deserializing this to DateTime
    }
}
    
    
