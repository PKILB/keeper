<template>
  <div class="about text-center bg-grey">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>


  <div class="container-fluid bg-grey container-ht">
    <div class="row">
      <div class="col-12 col-md-6 m-auto mt-5">
        <form @submit.prevent="editAccount()">
          <div class="form-floating mb-2">
            <input v-model="editable.picture" :placeholder="account.picture" required type="text" class="form-control"
              id="picture">
            <label for="floatingInput" class="form-label">Picture</label>
          </div>
          <div class="form-floating mb-2">
            <input v-model="editable.name" :placeholder="account.name" required type="text" class="form-control"
              id="name">
            <label for="floatingInput" class="form-label">Name</label>
          </div>
          <div class="form-floating mb-2">
            <input v-model="editable.coverImg" :placeholder="account.coverImg" required type="text" class="form-control"
              id="coverImg">
            <label for="floatingInput" class="form-label">CoverImg</label>
          </div>
          <button class="btn mb-4" type="submit">Save Changes</button>
        </form>
      </div>
    </div>


  </div>
</template>

<script>
// import { userInfo } from 'os'
import { computed, watchEffect, ref } from 'vue'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
export default {
  setup() {
    const editable = ref({})

    watchEffect(() => {
      if (AppState.profile.id == AppState.account.id) {
        editable.value = { ...AppState.profile }
      }
    })
    return {
      editable,
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),

      async editAccount() {
        try {
          const formData = editable.value
          await accountService.editAccount(formData)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
