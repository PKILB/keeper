<template>
    <div class="container my-1">
        <div class="card rounded elevation-1">
            <img class="img-fluid rounded" :src="vault.img" :alt="vault.name">
            <div class="card-img-overlay d-flex row">
                <div class="col-12">
                    <h6 class="text-light text-style">{{ vault.name }}</h6>
                </div>
            </div>
        </div>


    </div>
</template>


<script>
import { Vault } from '../models/Vault.js';

export default {
    props: { vault: { type: Vault, required: true } },
    setup() {
        return {}
    }
}
</script>


<style lang="scss" scoped>
.text-style {
    text-shadow: 0 0 4px rgb(0, 0, 0);
}
</style>