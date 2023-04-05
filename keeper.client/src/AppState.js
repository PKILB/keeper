import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},

  /** @type {import('./models/Account.js').Profile} */
  profile: {},
  
  /** @type {import('./models/Vault.js').Vault[]} */
  vaults: [], 
  
  /** @type {import('./models/Vault.js').Vault | null} */
  activeVault: null,

  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],
  
  /** @type {import('./models/KeptKeep.js').KeptKeep[]} */
  keptKeeps: [],

  /** @type {import('./models/KeptKeep.js').KeptKeep | null} */
  keptKeep: null,

  /** @type {import('./models/Keep.js').Keep | null} */
  keep: {},
  
/** @type {import('./models/Vault.js').Vault[]} */
myVaults: [],

/** @type {import('./models/VaultKeep.js').VaultKeep[]} */
  vaultKeep: []
  

})
