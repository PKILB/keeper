<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="bg-dark text-light">
    Made with 💖 by CodeWorks
  </footer>
  <CreateKeepModal id="createKeepModal">
    <KeepForm />
  </CreateKeepModal>
  <CreateVaultModal id="createVaultModal">
    <VaultForm />
  </CreateVaultModal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import CreateKeepModal from './components/CreateKeepModal.vue'
import CreateVaultModal from './components/CreateVaultModal.vue'
import KeepForm from './components/KeepForm.vue'
import Navbar from './components/Navbar.vue'
import VaultForm from './components/VaultForm.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, CreateKeepModal, KeepForm, CreateVaultModal, VaultForm }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
