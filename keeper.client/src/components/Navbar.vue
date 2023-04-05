<template>
  <nav class="navbar navbar-expand-lg navbar-dark px-3 border-style">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center ps-3">
        <button class="btn bg-info text-dark fw-bold">Home</button>
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <div v-if="account.id" class="dropdown">
          <button class="btn dropdown-toggle text-light" type="button" data-bs-toggle="dropdown" aria-expanded="false">
            Create
          </button>
          <ul class="dropdown-menu">
            <li @click="createKeep" data-bs-toggle="modal" data-bs-target="#createKeepModal" class="dropdown-item">new
              keep</li>
            <li @click="createVault" data-bs-toggle="modal" data-bs-target="#createVaultModal" class="dropdown-item">new
              vault</li>
          </ul>
        </div>
        <!-- <div class="text-center">
          the
          keepr
          co.
        </div> -->
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { AppState } from '../AppState.js';
import Login from './Login.vue'
import { computed } from 'vue';
export default {
  setup() {
    return {
      account: computed(() => AppState.account)

    }
  },
  components: { Login }
}
</script>

<style scoped>
.border-style {
  border-bottom: 4px solid rgba(0, 0, 0, 0.192);
}

a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
