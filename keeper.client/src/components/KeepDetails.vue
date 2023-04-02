<template>
    <div v-if="keep" class="container-fluid rounded">
        <div class="row">
            <div class="col-6 p-0">
                <img class="img-fluid w-100" :src="keep.img" alt="">
            </div>
            <div class="col-6 d-flex">
                <div class="row justify-content-center align-items-between">
                    <div>
                        this is where views and kepts will go
                    </div>
                    <h2 class="d-flex justify-content-center align-items-center">{{ keep.name }}</h2>
                    <p class="d-flex justify-content-center align-content-center px-5">{{ keep.description }}</p>
                    <div class="row">
                        <div class="col-4">
                            <h4 class="d-flex justify-content-center">Vaults</h4>
                        </div>
                        <div class="col-4">
                            <button class="btn btn-dark text-light rounded">save</button>
                        </div>
                        <div class="col-2">
                            <img class="profile-img" :src="keep?.creator?.picture" :alt="keep?.creator?.name">
                        </div>
                        <div class="col-2">
                            <p class="fw-bold">{{ keep?.creator?.name }}</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</template>


<script>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
export default {
    setup() {
        return {
            keep: computed(() => AppState.keep),
        }
    }
}
</script>


<style lang="scss" scoped>
.profile-img {
    border-radius: 50%;
    height: 7vh;
    width: 7vh;
}
</style>