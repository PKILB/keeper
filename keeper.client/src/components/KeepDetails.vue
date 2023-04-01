<template>
    <div v-if="keep">
        <div class="">Hello</div>

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


<style lang="scss" scoped></style>