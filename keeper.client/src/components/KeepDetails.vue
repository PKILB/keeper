<template>
    <div v-if="keep" class="container-fluid rounded">
        <div class="row">
            <div class="col-md-6 p-0 img-height">
                <img class="img-fluid w-100" :src="keep.img" alt="">
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="d-flex justify-content-center">
                        this is where views and kepts will go
                    </div>
                </div>
                <div class="row">
                    <h2 class="d-flex justify-content-center align-items-center">{{ keep.name }}</h2>
                    <p class="d-flex justify-content-center align-content-center px-5">{{ keep.description }}</p>
                </div>
                <div class="row d-flex align-content-end">
                    <div class="col">
                        <h4 class="">Vaults</h4>
                    </div>
                    <div class="col">
                        <button class="btn btn-dark text-light text-center rounded">save
                        </button>
                    </div>
                    <div class="col">
                        <img class="profile-img" :src="keep?.creator?.picture" :alt="keep?.creator?.name">
                        <!-- <router-link class="selectable" :to="{ name: 'Profile', params: { profileId: keep?.creatorId } }">
                        </router-link> -->
                    </div>
                    <div class="col">
                        <div>
                            <p class="fw-bold text-style p-0">{{ keep?.creator?.name }}</p>
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
import { Keep } from '../models/Keep.js';
export default {
    props: { keep: { type: Keep, required: true } },
    setup() {
        return {
            keep: computed(() => AppState.keep),
            // profile: computed(() => AppState.profile)
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

.text-style {
    font-size: 13px;
}

.btn-size {
    height: 30px;
    width: 50px;
}

.img-height {
    max-height: 100%;
    position: relative;
}
</style>