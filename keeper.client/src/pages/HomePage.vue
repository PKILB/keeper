<template>
  <div class="container-fluid py-2">
    <div class="row">
      <div class="col-md-10 m-auto">
        <section class="bricks">
          <div v-for="k in keeps" class="pt-2">
            <KeepCard :keep="k" />
          </div>
        </section>
      </div>
    </div>
  </div>

  <KeepModalComponent id="keepDetails">
    <KeepDetails />
  </KeepModalComponent>
</template>

<script>
import { onMounted, computed, watchEffect } from 'vue';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';
import KeepCard from '../components/KeepCard.vue';
// import KeepDetails from '../components/KeepDetails.vue';

export default {
  setup() {
    watchEffect(() => {
      getAllKeeps();
    });
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      }
      catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
$gap: .5em;

.bricks {
  columns: 300px;
  column-gap: $gap;

  &>div {
    margin-top: $gap;
    display: inline-block;
  }
}

@media screen and (max-width: 667px) {
  .bricks {
    columns: 150px;
    column-gap: $gap;

    &>div {
      margin-top: $gap;
      display: inline-block;
    }
  }
}
</style>
